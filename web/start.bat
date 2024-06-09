cd app
START "StreetFinderServer" streetfinder.exe
timeout 10 > NUL
start msedge --new-window http://localhost:5000