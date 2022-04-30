# get the base node image
FROM node:alpine as builder

# set the working dir for container
WORKDIR /frontend

# copy the json file first
COPY package.json /frontend

# install npm dependencies
RUN npm install

# copy other project files
COPY . .

# build the folder
CMD [ "npm", "run", "start" ]

#docker build --tag react .
#docker run react





#FROM node:14.9.0 AS build-step 

#WORKDIR /build 
#COPY package.json package-lock.json ./ 
#RUN npm install 

#COPY . . 
#RUN npm run build 

#FROM nginx:1.18-alpine 
#COPY nginx.conf /etc/nginx/nginx.conf 
#COPY --from=build-step /build/build /frontend/build

#From ubuntu
#COPY ../ app
#ENV DEBIAN_FRONTEND=noninteractive
#RUN apt-get update && apt-get install -y npm
#WORKDIR app
#RUN npm install


