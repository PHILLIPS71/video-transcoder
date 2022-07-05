import type { BreakpointValue, Breakpoints } from '@/themes/abstrations/breakpoints'

import React from 'react'

export type Responsive<T> = T | Array<T>

export const getMediaQuery = (breakpoint: BreakpointValue): string => `@media screen and (min-width: ${breakpoint.min})`

export const getMediaQueries = (breakpoints: Breakpoints): string[] => Object.values(breakpoints).map(getMediaQuery)

export const getResponsiveValue = <T>(responsive: Responsive<T>, i: number): T => {
  if (!Array.isArray(responsive)) {
    return responsive
  }

  if (typeof responsive[i] === 'undefined') {
    return responsive[responsive.length - 1]
  }

  return responsive[i]
}

export const getResponsiveNumber = <T>(responsive: Responsive<T>, i: number): number => {
  const value = getResponsiveValue(responsive, i)
  return typeof value === 'number' ? value : 0
}

export const useWindowSize = () => {
  type WindowSize = {
    width: number
    height: number
  }

  const [size, setSize] = React.useState<WindowSize>({ width: 0, height: 0 })

  const onResize = () => {
    setSize({
      width: window.innerWidth,
      height: window.innerHeight,
    })
  }

  React.useEffect(() => {
    onResize()

    window.addEventListener('resize', onResize)

    return () => window.removeEventListener('resize', onResize)
  }, [])

  return size
}
