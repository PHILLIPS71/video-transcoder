import { i18n } from '@lingui/core'
import { I18nProvider } from '@lingui/react'
import { de, en } from 'make-plural'
import React from 'react'

import { Locale } from '@/contexts/i18n/Local'

i18n.loadLocaleData({
  en: { plurals: en },
  de: { plurals: de },
})

export type LocaleContext = {
  locale?: Locale
  setLocale: React.Dispatch<React.SetStateAction<Locale>>
}

type LocaleProviderProps = {
  locale?: Locale
  children: React.ReactElement | React.ReactElement[]
}

const Context = React.createContext<LocaleContext>({
  locale: Locale.English,
  setLocale: () => null,
})

export const LocaleProvider: React.FC<LocaleProviderProps> = ({ children, ...props }) => {
  const [locale, setLocale] = React.useState(props.locale ?? Locale.English)

  React.useEffect(() => {
    import(`../../locales/${locale}/messages`).then((module) => {
      const { messages } = module

      i18n.load(locale, messages)
      i18n.activate(locale)
    })
  }, [locale])

  const value = React.useMemo(
    () => ({
      locale,
      setLocale,
    }),
    [locale]
  )

  return (
    <Context.Provider value={value}>
      <I18nProvider i18n={i18n}>{children}</I18nProvider>
    </Context.Provider>
  )
}

LocaleProvider.defaultProps = {
  locale: Locale.English,
}

export default Context
