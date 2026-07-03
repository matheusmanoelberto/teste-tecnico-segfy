import type { ButtonHTMLAttributes, ReactNode } from 'react'

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: 'primary' | 'secondary' | 'danger'
  children: ReactNode
}

export function Button({ variant = 'primary', children, ...props }: ButtonProps) {
  return (
    <button className={`button button-${variant}`} type="button" {...props}>
      {children}
    </button>
  )
}
