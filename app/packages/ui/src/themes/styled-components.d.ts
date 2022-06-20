import type { Colours } from '@/themes/abstrations/colours'

import 'styled-components'

declare module 'styled-components' {
  export interface DefaultTheme extends Theme {
    fonts: {
      text: string
    }
    colours: Colours
  }
}
