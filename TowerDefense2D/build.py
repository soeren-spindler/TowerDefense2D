import subprocess
import shutil
import os
import zipfile
import threading

print('Cleaning build directory')

shutil.rmtree('Builds', ignore_errors=True)

print('Starting build process')

UNITY = '"C:\\Program Files\\Unity\\Hub\\Editor\\2020.2.7f1\\Editor\\Unity.exe"'

subprocess.run(
    f'{UNITY} -quit -projectPath -batchmode {os.getcwd()} -executeMethod Build.BuildAll')

print('- Complete')


def zipdir(path):
    ziph = zipfile.ZipFile(f'{os.path.basename(path)}.zip', 'w', zipfile.ZIP_DEFLATED)
    for root, dirs, files in os.walk(path):
        for file in files:
            ziph.write(os.path.join(root, file))


print('Creating build archives')
zipdir('Builds/Windows')
# zipdir('Builds/Linux')
# zipdir('Builds/Web GL')
# zipdir('Builds/MacOSX.app')
print('- Complete')