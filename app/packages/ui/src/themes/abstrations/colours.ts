export type Foundation = 'primary' | 'secondary' | 'tertiary'

type FoundationColour = {
  [K in Foundation as K]: string
}

export type Colours = FoundationColour & {
  text: FoundationColour
  background: FoundationColour
}
