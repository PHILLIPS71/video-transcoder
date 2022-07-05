import type { Breakpoints } from '@/themes/abstrations/breakpoints'
import type { Colours } from '@/themes/abstrations/colours'

import 'styled-components'

declare module 'styled-components' {
  export interface DefaultTheme {
    layout: {
      width: string
      breakpoints: Breakpoints
    }
    fonts: {
      text: string
    }
    colors: Colours
  }
}
