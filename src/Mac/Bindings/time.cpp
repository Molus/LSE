#include <GL/glfw.h>

#include "time.h"

extern "C" double GetElapsedTime()
{
	return glfwGetTime();
}	