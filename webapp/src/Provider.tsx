import React from 'react'
import { Provider as ReduxProvider } from 'react-redux'
import { HelmetProvider } from 'react-helmet-async'
import { CookiesProvider } from 'react-cookie'
import { I18nextProvider } from 'react-i18next'
import { useSelector } from 'react-redux'
import { ThemeProvider, createTheme } from '@mui/material/styles'
import { PersistGate } from 'redux-persist/integration/react'
import { store, persistor } from './stote'
import { themeSelector } from './stote/selectors'
import i18n from './locales/I18n'
import Loader from './components/loader'

// define interface to represent component props
interface Props {
  children: React.ReactNode
}

const Provider = ({ children }: Props) => {
  return (
    <HelmetProvider>
      <CookiesProvider>
        <ReduxProvider store={store}>
          <PersistGate loading={<Loader />} persistor={persistor}>
            <I18nextProvider i18n={i18n}>
              <ThemeWrapper>{children}</ThemeWrapper>
            </I18nextProvider>
          </PersistGate>
        </ReduxProvider>
      </CookiesProvider>
    </HelmetProvider>
  )
}

const ThemeWrapper = ({ children }: Props) => {
  const selectedTheme = useSelector(themeSelector)
  const currentTheme = React.useMemo(() => createTheme(selectedTheme), [selectedTheme])
  return <ThemeProvider theme={currentTheme}>{children}</ThemeProvider>
}

export default Provider
