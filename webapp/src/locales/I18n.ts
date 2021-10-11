import i18next from 'i18next'
import { initReactI18next } from 'react-i18next'
import LanguageDetector from 'i18next-browser-languagedetector'

import translationEN from './en/translation.json'
import translationVN from './vi/translation.json'

const i18n = i18next.use(initReactI18next)
i18n.use(LanguageDetector).init({
  lng: 'en',
  resources: {
    en: {
      translation: translationEN,
    },
    vi: {
      translation: translationVN,
    },
  },
  initImmediate: false,
  fallbackLng: 'en',

  // string or array of namespaces to load
  ns: ['translation'],
  defaultNS: 'translation',

  interpolation: {
    escapeValue: false, // not needed for react
    formatSeparator: ',',
  },

  // react-i18next special options (optional)
  react: {
    wait: true, // true: wait for loaded in every translated hoc
  },
})

export default i18n
