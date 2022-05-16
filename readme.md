### To up the service and dependencies ###
`Ex.: docker-compose up -d`

<br />

#### To mount an image ####
`docker build . -f ./<DOCKER_FILE> -t <TAG_NAME>:<TAG_VERSION> --no-cache`

`Ex.: docker build . -f ./Dockerfile -t pay-api:v1.0.0 --no-cache`
<br />

#### To run an imagem ####
`docker run --name <CONTAINER_NAME> -d -p <HOST_COMPUTER_PORT>:<INTERNAL_CONTAINER_PORT> <TAG_NAME>:<TAG_VERSION>`

`Ex.: docker run --name pay-api -d -p 9000:9000 pay-api:v1.0.0 --restart=always`
<br />