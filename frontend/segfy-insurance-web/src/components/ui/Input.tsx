import type { InputHTMLAttributes } from 'react'

type InputProps = InputHTMLAttributes<HTMLInputElement> & {
  label: string
}

export function Input({ label, id, ...props }: InputProps) {
  return (
    <label className="form-field" htmlFor={id}>
      <span>{label}</span>
      <input id={id} {...props} />
    </label>
  )
}
