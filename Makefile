TARGET = mueb-utility

mueb-utility: matrix4MuebUtility/*.cs
	mcs $? -out:$(TARGET)

.PHONY: clean

clean: 
	rm -f $(TARGET)

all: mueb-utility
