# pull official base image
FROM node:lts AS builder

ENV NODE_ENV production
ENV GENERATE_SOURCEMAP false
ENV REACT_APP_SERVER_URL=${REACT_APP_SERVER_URL}

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY yarn.lock ./

RUN yarn install --production

# add app
COPY . ./

# start app
RUN yarn build --profile

# Bundle static assets with nginx
FROM nginx:stable as production
ENV NODE_ENV production
# Copy built assets from builder
COPY --from=builder /app/build /usr/share/nginx/html
# Add your nginx.conf
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf
# Expose port
EXPOSE 80
# Start nginx
CMD ["nginx", "-g", "daemon off;"]