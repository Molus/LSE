#ifndef __INPUTS_H__
#define __INPUTS_H__

extern "C" bool GetIsKeyPressed(int key);

extern "C" void GetMousePosition(int *x, int *y);

extern "C" bool GetIsMouseButtonPressed(int button);

#endif