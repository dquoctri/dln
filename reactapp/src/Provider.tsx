import React from "react"
import { Provider as ReduxProvider } from "react-redux"
import { HelmetProvider } from "react-helmet-async"
import { CookiesProvider } from "react-cookie"
import { I18nextProvider } from "react-i18next"
import { PersistGate } from "redux-persist/integration/react"
import { store, persistor } from "./stote"
import i18n from "./assets/locales/I18n"

// define interface to represent component props
interface Props {
  children: React.ReactNode
}

const Provider = ({ children }: Props) => {
  return (
    <HelmetProvider>
      <CookiesProvider>
        <ReduxProvider store={store}>
          <PersistGate loading={null} persistor={persistor}>
            <I18nextProvider i18n={i18n}>{children}</I18nextProvider>
          </PersistGate>
        </ReduxProvider>
      </CookiesProvider>
    </HelmetProvider>
  )
}

export default Provider
