import React from 'react'
import { useMediaQuery } from 'react-responsive'

import { Block } from '@giantnodes/ui'

import NavigationBar from '@/layouts/components/NavigationBar'
import NavigationSidebar from '@/layouts/components/NavigationSidebar'

type DefaultLayoutProps = {
  children: React.ReactNode
}

type DefaultLayoutContext = {
  isMobile: boolean
  isSidebarOpen: boolean
  setSidebarOpen: React.Dispatch<React.SetStateAction<boolean>>
}

type Layout = React.FC<DefaultLayoutProps> & {
  Consumer: React.Consumer<DefaultLayoutContext>
}

const { Provider, Consumer } = React.createContext<DefaultLayoutContext>({
  isMobile: false,
  isSidebarOpen: true,
  setSidebarOpen: () => null,
})

const DefaultLayout: Layout = ({ children }) => {
  const isMobile = useMediaQuery({ maxWidth: '768px' })
  const [isSidebarOpen, setSidebarOpen] = React.useState<boolean>(!isMobile)

  React.useEffect(() => {
    setSidebarOpen(!isMobile)
  }, [isMobile])

  return (
    <Provider value={{ isMobile, isSidebarOpen, setSidebarOpen }}>
      <Block display="flex" flexDirection="column" height="100%">
        <Block position="relative">
          <NavigationBar />
        </Block>

        <Block display="flex" position="relative" flex="1 1 0%" width="100%" minHeight="0px">
          <Block display="flex" position="relative" flex="1 1 0%" flexDirection="row" overflow="hidden">
            <NavigationSidebar />

            <Block overflow="auto" width="100%" padding="32px">
              {children}
            </Block>
          </Block>
        </Block>
      </Block>
    </Provider>
  )
}

DefaultLayout.Consumer = Consumer

export default DefaultLayout
