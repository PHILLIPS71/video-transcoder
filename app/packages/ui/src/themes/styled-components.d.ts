import type { Colours } from '@/themes/abstrations/colours'

import 'styled-components'

declare module 'styled-components' {
  export interface DefaultTheme {
    fonts: {
      text: string
    }
    colors: Colours
  }
}
