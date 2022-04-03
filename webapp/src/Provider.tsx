import React, { ReactNode, Suspense } from 'react'
import { Provider as ReduxProvider } from 'react-redux'
import { CookiesProvider } from 'react-cookie'
import { I18nextProvider } from 'react-i18next'
import { PersistGate } from 'redux-persist/integration/react'
import { HelmetProvider } from 'react-helmet-async'
import { store, persistor } from './stote'
import i18n from './locales/I18n'
import Loader from './components/loader'

// define interface to represent component props
interface Props {
  children: ReactNode
}

const Provider = ({ children }: Props) => {
  return (
    <CookiesProvider>
      <ReduxProvider store={store}>
        <HelmetProvider>
          <PersistGate loading={<Loader />} persistor={persistor}>
            <Suspense fallback={<Loader />}>
              <I18nextProvider i18n={i18n}>
                {children}
              </I18nextProvider></Suspense>
          </PersistGate>
        </HelmetProvider>
      </ReduxProvider>
    </CookiesProvider>
  )
}

export default Provider
