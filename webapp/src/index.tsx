import React from 'react'
import ReactDOM from 'react-dom'
import Provider from './Provider'
import App from './App'

const rootElement = document.getElementById('root')
const appElement = (
  <React.StrictMode>
    <Provider>
      <App />
    </Provider>
  </React.StrictMode>
)

ReactDOM.render(appElement, rootElement)
