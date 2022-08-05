module.exports = {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
    autoprefixer: {},
    ...(process.env.NODE_ENV === 'production' ? { cssnano: {} } : {})
  },
}
