import type { DefaultTheme } from 'styled-components'

const LightTheme: DefaultTheme = {
  layout: {
    width: '1408px',
    breakpoints: {
      mobile: {
        min: '0px',
        max: '768px',
      },
      tablet: {
        min: '769px',
        max: '1023px',
      },
      desktop: {
        min: '1024px',
        max: '1215px',
      },
      widescreen: {
        min: '1216px',
        max: '100vw',
      },
    },
  },
  fonts: {
    text: 'Lato, sans-serif',
  },
  colors: {
    primary: '#23a166',
    secondary: '#001e26',
    tertiary: '#06f4b6',

    black: '#000000',
    white: '#ffffff',

    text: {
      primary: '#000000',
      secondary: '#838691',
      tertiary: '#dbdbdb',
    },

    background: {
      primary: '#ffffff',
      secondary: '#f2f2f2',
      tertiary: '#ffffff',
    },
  },
}

export default LightTheme
