export type Foundation = 'primary' | 'secondary' | 'tertiary'

type FoundationColour = {
  [K in Foundation as K]: string
}

export type Colours = FoundationColour & {
  black: string
  white: string
  text: FoundationColour
  background: FoundationColour
}
