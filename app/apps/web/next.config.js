const withTM = require('next-transpile-modules')(['@giantnodes/ui'])

module.exports = withTM({
  reactStrictMode: true,
  compiler: {
    styledComponents: true,
  },
})
