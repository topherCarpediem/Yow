FROM node:12

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

COPY src/clients/yow-react-client/package.json /app/package.json

RUN yarn install

CMD ["yarn", "start"]