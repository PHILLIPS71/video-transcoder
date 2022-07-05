import type { DefaultTheme } from 'styled-components'

const DarkTheme: DefaultTheme = {
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
      primary: '#ffffff',
      secondary: '#a2a5b9',
      tertiary: '#334056',
    },

    background: {
      primary: '#151a23',
      secondary: '#1a212c',
      tertiary: '#262f40',
    },
  },
}

export default DarkTheme
