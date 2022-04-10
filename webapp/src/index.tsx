import React from 'react'
import ReactDOM from 'react-dom/client'
import Provider from './Provider'
import App from './App'
import { env } from './utils'

const isStrictMode = env.NODE_ENV === 'development'
const Wrapper = isStrictMode ? React.StrictMode : React.Fragment
const appElement = (
  <Wrapper>
    <Provider>
      <App />
    </Provider>
  </Wrapper>
)

const rootElement = document.getElementById('root')
const root = rootElement ? ReactDOM.createRoot(rootElement) : null
root && root.render(appElement)
