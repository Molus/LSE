#include <GL/glew.h>
#include <GL/glfw.h>

#include "graphics.h"

GLint viewportWidth;
GLint viewportHeight;

extern "C" bool InitializeViewport(int width, int height, bool fullscreen)
{
	// Initialise GLFW
	if(!glfwInit())
	{
		return false;
	}
	
	int windowMode = (fullscreen ? GLFW_FULLSCREEN : GLFW_WINDOW);
		
	// Open a window and create its OpenGL context
	glfwOpenWindowHint(GLFW_FSAA_SAMPLES, 4);
    if(!glfwOpenWindow(width, height, 8, 8, 8, 8, 24, 0, windowMode))
	{
		glfwTerminate();
		return false;
	}
	
    // Ensure we can capture the escape key being pressed below
	glfwEnable(GLFW_STICKY_KEYS);
    
	// Initialize GLEW
	if(glewInit() != GLEW_OK) {
		return false;
	}
	
	// Setup our screen
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, width, 0, height, -100, 100);
	glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
	glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
	
	// Enable z-buffer
	glEnable(GL_DEPTH_TEST);
	glDepthMask(GL_TRUE);
	glDepthFunc(GL_LEQUAL);
		
	// Set the general polygon properties
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	glAlphaFunc(GL_GREATER, 0.1f);
	glEnable(GL_TEXTURE_2D);
	glEnable(GL_BLEND);
    glEnable(GL_ALPHA_TEST);
    
    viewportWidth = width;
    viewportHeight = height;
    
    return true;
}


unsigned int lastTextureId;

extern "C" void DrawTexture2D(unsigned int textureId, float xPos, float yPos, float zPos, float rotation,
	float texLeft, float texTop, float texRight, float texBottom, float width, float height)
{
	float halfWidth = (width / 2);
	float halfHeight = (height / 2);
    float xDrawPos = (xPos + halfWidth);
    float yDrawPos = (viewportHeight -halfHeight - yPos);
	
	if (textureId != lastTextureId)
	{
		lastTextureId = textureId;
    	glBindTexture(GL_TEXTURE_2D, textureId);
	}
    
    glPushMatrix();
        
    glTranslatef(xDrawPos, yDrawPos, 0.0f);
    glRotatef(rotation, 0.0f, 0.0f, 1.0f);
             
	glBegin(GL_QUADS);
					
    glTexCoord2f(texLeft, texTop);
    glVertex3i(-halfWidth, halfHeight, zPos);
    
    glTexCoord2f(texLeft, texBottom);
    glVertex3i(-halfWidth, -halfHeight, zPos);
    
    glTexCoord2f(texRight, texBottom);
    glVertex3i(halfWidth, -halfHeight, zPos);
    
    glTexCoord2f(texRight, texTop);
    glVertex3i(halfWidth, halfHeight, zPos);
              
	glEnd();
       
    glPopMatrix();
}


extern "C" void CleanBackBuffer()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
}


extern "C" void SwapBackBuffer()
{
	glfwSwapBuffers();	
}

	
extern "C" bool GetIsWindowOpened()
{
	return glfwGetWindowParam(GLFW_OPENED);
}
	
	
extern "C" void ReleaseViewport()
{
	glfwTerminate();
}