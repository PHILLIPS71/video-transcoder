export type Breakpoint = 'mobile' | 'tablet' | 'desktop' | 'widescreen'

export type BreakpointValue = {
  min: string
  max: string
}

export type Breakpoints = {
  [K in Breakpoint]: BreakpointValue
}
