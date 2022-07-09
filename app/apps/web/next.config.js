const { locales, sourceLocale } = require('./lingui.config')
const withTM = require('next-transpile-modules')(['@giantnodes/ui'])

module.exports = withTM({
  i18n: {
    locales,
    defaultLocale: sourceLocale,
  },
  reactStrictMode: true,
  compiler: {
    styledComponents: true,
  },
})
