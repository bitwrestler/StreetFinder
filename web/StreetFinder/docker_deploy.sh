!#/usr/bin/bash
LABEL=street_finder
docker rm -f $LABEL
docker build -t $LABEL -f Dockerfile . &&  docker run -d -p 5002:5216 --name $LABEL $LABEL

