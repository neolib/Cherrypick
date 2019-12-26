/*
 * Keyboard helper functions.
 *
 * Copyright © neolib.net. All rights reserved.
 *
 * */
#include "pch.h"
#include "Helper.h"

/*
The extended-key flag indicates whether the keystroke message originated from one of the additional
keys on the enhanced keyboard. The extended keys consist of the ALT and CTRL keys on the right-hand
side of the keyboard; the INS, DEL, HOME, END, PAGE UP, PAGE DOWN, and arrow keys in the clusters to
the left of the numeric keypad; the NUM LOCK key; the BREAK (CTRL+PAUSE) key; the PRINT SCRN key;
and the divide (/) and ENTER keys in the numeric keypad.
*/

BOOL IsExtendedKey(BYTE vk)
{
	static BYTE aKeys[] = { VK_RCONTROL, VK_RSHIFT, VK_RWIN, VK_RCONTROL,
		VK_INSERT, VK_DELETE, VK_HOME, VK_END, VK_PRIOR, VK_NEXT,
		VK_UP, VK_DOWN, VK_LEFT, VK_RIGHT, VK_NUMLOCK, VK_PAUSE, VK_PRINT,
		VK_DIVIDE, 0 };

	for (int i = 0; aKeys[i]; i++)
	{
		if (vk == aKeys[i]) return TRUE;
	}
	return FALSE;
}

void SendKeyAction(BYTE vk, bool up, UINT repeat)
{
	INPUT input = { 0 };
	input.type = INPUT_KEYBOARD;
	input.ki.wVk = vk;
	input.ki.dwFlags = up ? KEYEVENTF_KEYUP : 0;
	if (IsExtendedKey(vk)) input.ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
	while (repeat > 0)
	{
		SendInput(1, &input, sizeof(INPUT));
		repeat--;
	}
}

void SendKey(BYTE vk, UINT repeat)
{
	INPUT inputs[2] = { 0 };
	inputs[0].type = INPUT_KEYBOARD;
	inputs[0].ki.wVk = vk;

	inputs[1] = inputs[0];
	inputs[1].ki.dwFlags |= KEYEVENTF_KEYUP;

	while (repeat > 0)
	{
		SendInput(2, inputs, sizeof(INPUT));
		repeat--;
	}
}

BOOL WINAPI SaveKeyboardState(PBYTE buffer)
{
	static BYTE aModVk[] = {
		VK_LMENU, VK_RMENU, VK_LCONTROL, VK_RCONTROL, VK_LSHIFT, VK_RSHIFT, 0 };

	if (GetKeyboardState(buffer))
	{
		/* Don't know why checking against buffer[VK] does not work... */
		if (GetKeyState(VK_CAPITAL) & 1) SendKey(VK_CAPITAL);

		for (int i = 0; aModVk[i]; i++)
		{
			if (GetKeyState(aModVk[i]) & 0x80) SendKeyUp(aModVk[i]);
		}
		return TRUE;
	}
	return FALSE;
}

BOOL WINAPI RestoreKeyboardState(PBYTE state)
{
	return SetKeyboardState(state);
}

