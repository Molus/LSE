#include <GL/glew.h>
#include <GL/glfw.h>

#include "textures.h"
#include "lodepng.h"


extern "C" TextureInfo LoadTexture2DFromMemory(void *data, unsigned int size)
{
	// TODO: Check for valid data and device initialization
	TextureInfo loadedTexture;
	LodePNG::Decoder decoder;
	std::vector<unsigned char> image;
	
    decoder.decode(image,(const unsigned char*)data, size);
    loadedTexture.width = decoder.getWidth();
    loadedTexture.height = decoder.getHeight();
    
	// TODO: Check for image.empty()
	glGenTextures(1, &loadedTexture.textureId);	
    glBindTexture(GL_TEXTURE_2D, loadedTexture.textureId);
    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, loadedTexture.width, loadedTexture.height, 0, GL_RGBA, GL_UNSIGNED_BYTE, &image[0]);
    
    // Specify filtering
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    
    // Edge actions not needed for sprites?
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
    
    return loadedTexture;
}


extern "C" TextureInfo LoadTexture2DFromFile(const char* path)
{
	std::vector<unsigned char> data;
	
    LodePNG::loadFile(data, path);
    
    // TODO: Check for loading failures
    return LoadTexture2DFromMemory(&data[0],(unsigned)data.size());
}


extern "C" void UnloadTexture(unsigned int textureId)
{
	// TODO: Check graphics device initialization
	glDeleteTextures(1, &textureId);
}