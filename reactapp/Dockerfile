# pull official base image
FROM node:lts AS builder

ENV NODE_ENV development

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY yarn.lock ./

RUN yarn

# add app
COPY . ./

EXPOSE 3000

# start app
CMD ["yarn", "start"]