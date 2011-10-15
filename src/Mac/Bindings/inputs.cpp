#include <GL/glfw.h>

#include "inputs.h"

extern "C" bool GetIsKeyPressed(int key)
{
	return (GLFW_PRESS == glfwGetKey(key));
}

extern "C" void GetMousePosition(int *x, int *y)
{
	glfwGetMousePos(x, y);
}

extern "C" bool GetIsMouseButtonPressed(int button)
{
	return (GLFW_PRESS == glfwGetMouseButton(button));
}