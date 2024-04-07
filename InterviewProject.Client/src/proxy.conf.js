const { env } = require('process');
const winston = require("winston");

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7240';

function logProvider() {
  return winston.createLogger({
    level: "debug",
    format: winston.format.combine(
        winston.format.splat(),
        winston.format.simple()
    ),
    transports: [new winston.transports.Console()]
  })
}

const PROXY_CONFIG = [
  {
    context: [
     "/api"
    ],
    target,
    ws: true,
    secure: false,
    logLevel: "debug",
    logProvider: logProvider,
  }
]

module.exports = PROXY_CONFIG;
