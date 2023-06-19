# To Recreate this project, follow this Guide:

1. Install the Latest version of Unity 

2. In Unity, you also need to install MLagents via the Package Manager. Please install ML Agents 2.01 or above

3. Install Python 3.9.13 

[SIDE NOTE]:

Please Google the latest version.

4. Install CUDA 12.1.1 Release

[SIDE NOTE]:

Please Google the latest version. 

5. Enter your Command Prompt and switch to the directory of your Unity Project you wish to use ML Agents for.

 In Command Prompt (Windows 11 64 bit):
	
	cd [Copy and Paste Address here]

6. Create a virtual environment in your Unity Folder

 In Command Prompt (Windows 11 64 bit):
	
	python -m venv venv

[SIDE NOTE]:

The second venv in the command line is your folder name, you can name it whatever you want; venv keeps it simple. You can verify this command worked by going to your file explorer and looking for the venv folder.

7. Enter the virtual environment.

 In Command Prompt (Windows 11 64 bit):
	
	venv\Scripts\activate

[SIDE NOTE]:

You will know you are in your virtual environment when it says (venv) next to your directory location.

8. Now you want to run this command:

In Command Prompt (Windows 11 64 bit):
	
	python -m pip install --upgrade pip

9. Install Pytorch 2.0 

In Command Prompt (Windows 11 64 bit):
	
	pip3 install torch torchvision torchaudio --index-url https://download.pytorch.org/whl/cu118

For other versions use this link:

https://pytorch.org/get-started/pytorch-2.0/ 


10. Install ML Agents Package

In Command Prompt (Windows 11 64 bit):	
	
	pip install mlagents

To Verify this worked type:

	mlagents-learn --help

[SIDE NOTE]:
	
Just in case you are trying to run this code and you get an error that says:

TypeError: Descriptors can not be created directly.
If this call came from a _pb2.py file, your generated code is out of date and must be regenerated with protoc >= 3.19.0.
If you cannot immediately regenerate your protos, some other possible workarounds are:
 1. Downgrade the protobuf package to 3.20.x or lower.
 2. Set PROTOCOL_BUFFERS_PYTHON_IMPLEMENTATION=python (but this will use pure-Python parsing and will be much slower).

What you want to do in your Command Prompt is type this:

	pip install protobuf==3.20

Then rerun this command to verify:

	mlagents-learn --help

In case you are having with the installation guide you can watch this video by CodeMonkey that helps with creating your first Unity Project with ML agents.

REFERENCE:

CodeMonkey. (2020, November 29). CodeMonkey Introduction [Video file]. Retrieved from https://www.youtube.com/watch?v=zPFU30tbyKs&ab_channel=CodeMonkey
