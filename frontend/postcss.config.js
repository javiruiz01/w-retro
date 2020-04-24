const purgecss = require('@fullhuman/postcss-purgecss')({
  content: ['./**/*.html', './**/*.svelte'],
  defaultExtractor: (content) => content.match(/[A-Za-z0-9-_:/]+/g) || [],
  whitelist: ['left-shadow', 'right-shadow'],
});

const production = !process.env.ROLLUP_WATCH;

module.exports = {
  plugins: [require('tailwindcss'), ...(production ? [purgecss] : [])],
};
