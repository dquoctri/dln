# Getting Started with Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `yarn start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `yarn test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `yarn build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `yarn eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).


# update latest all dependencies

yarn add @reduxjs/toolkit @testing-library/jest-dom @testing-library/react @testing-library/user-event
yarn add @types/jest @types/node @types/react @types/react-dom @types/react-redux @types/react-router-dom @types/redux-logger @typescript-eslint/parser
yarn add i i18next i18next-browser-languagedetector jwt-decode prop-types
yarn add react react-cookie react-dom react-helmet-async react-i18next react-redux react-router-dom react-scripts
yarn add redux redux-logger redux-persist redux-thunk typescript
yarn add --dev @typescript-eslint/eslint-plugin eslint eslint-plugin-react prettier

# format code with pretier
yarn run prettier --write src

yarn eslint --init

Default breakpoints
xs, extra-small: 0px
sm, small: 600px
md, medium: 900px
lg, large: 1200px
xl, extra-large: 1536px


src/
├── sql/
│   ├── data/
│   │   ├── MyTable.csv
│   ├── table/
│   │   ├── MyTable.sql
│   ├── entrypoint.sh
│   ├── init.sql
├── docker-compose.yml
