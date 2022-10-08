import i18next, { i18n as i18nInstance } from "i18next"
import { initReactI18next } from "react-i18next"
import LanguageDetector from "i18next-browser-languagedetector"

import translationEN from "./en/translation.json"
import translationVN from "./vi/translation.json"

const createI18n = (language: string): i18nInstance => {
  const i18n = i18next.createInstance().use(initReactI18next).use(LanguageDetector)
  i18n.init({
    lng: language,
    resources: {
      en: {
        translation: translationEN,
      },
      vi: {
        translation: translationVN,
      },
    },
    initImmediate: false,
    fallbackLng: language,
    ns: ["translation"],
    defaultNS: "translation",
    interpolation: {
      escapeValue: false,
      formatSeparator: ",",
    },
    react: {
      useSuspense: true,
    },
  })
  return i18n
}

const i18n = createI18n("en")

export default i18n
