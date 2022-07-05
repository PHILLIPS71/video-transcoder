import React from 'react'
import { Moon, Sun } from 'react-feather'
import styled from 'styled-components'

const Label = styled.label`
  width: 54px;
  display: block;
  position: relative;
  cursor: pointer;
  font-size: 22px;
  user-select: none;
  transform: scale(0.9);
`

const ThemeOptionContainer = styled.span`
  position: relative;
  display: block;
  height: 28px;
  width: 51px;
  border: 1px solid #e0e0e0;
  background: transparent;
  box-shadow: -1px 3px 10px 0 rgba(0, 0, 0, 0.06);
  border-radius: 100px;
  transition: all 0.3s;
`

const ThemeOption = styled.span`
  position: absolute;
  top: 2px;
  left: 2px;
  height: 24px;
  width: 24px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  transform: translateX(0) rotate(0);
  transition: all 0.3s ease;

  svg {
    stroke: #edc31c;
    height: 14px;
    width: 14px;
    opacity: 1;
  }
`

const DarkThemeOption = styled(ThemeOption)`
  background: #5596e6;
  border-color: #5596e6;
  opacity: 0;
  z-index: 0;

  svg {
    stroke: ${(props) => props.theme.colors.white};
  }
`

const LightThemeOption = styled(ThemeOption)`
  background: ${(props) => props.theme.colors.white};
  border-color: #dedede;
  opacity: 1;
  z-index: 1;
`

const Checkbox = styled.input`
  position: absolute;
  opacity: 0;
  cursor: pointer;

  &:checked {
    & ~ ${ThemeOptionContainer} {
      border-color: #5596e6;

      ${DarkThemeOption}, ${LightThemeOption} {
        transform: translateX(98%) rotate(360deg);
      }

      ${DarkThemeOption} {
        opacity: 1;
      }

      ${LightThemeOption} {
        opacity: 0;
      }
    }
  }
`

type ThemeToggleProps = React.HTMLProps<HTMLInputElement>

const ThemeToggle: React.FC<ThemeToggleProps> = ({ checked, onChange }) => (
  <Label>
    <Checkbox type="checkbox" checked={checked} onChange={onChange} />
    <ThemeOptionContainer>
      <DarkThemeOption>
        <Moon />
      </DarkThemeOption>
      <LightThemeOption>
        <Sun />
      </LightThemeOption>
    </ThemeOptionContainer>
  </Label>
)

export default ThemeToggle
