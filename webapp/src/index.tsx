import React from 'react'
import ReactDOM from 'react-dom/client'
import Provider from './Provider'
import App from './App'

const container = document.getElementById('root')
const root = container ? ReactDOM.createRoot(container) : null
const appElement = (
  <React.StrictMode>
    <Provider>
      <App />
    </Provider>
  </React.StrictMode>
)

root && root.render(appElement)
