#pragma once

void SendKeyAction(BYTE vk, bool up, UINT repeat = 1);
void SendKey(BYTE vk, UINT repeat = 1);
inline void SendKeyDown(BYTE vk, UINT repeat = 1) { SendKeyAction(vk, false, repeat); }
inline void SendKeyUp(BYTE vk, UINT repeat = 1) { SendKeyAction(vk, true, repeat); }

EXTERN_C_START
__declspec(dllexport) BOOL WINAPI SaveKeyboardState(PBYTE buffer);
__declspec(dllexport) BOOL WINAPI RestoreKeyboardState(PBYTE buffer);
EXTERN_C_END
