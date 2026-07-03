import type { SelectHTMLAttributes } from 'react'

type SelectProps = SelectHTMLAttributes<HTMLSelectElement> & {
  label: string
  options: Array<{ label: string; value: string }>
}

export function Select({ label, id, options, ...props }: SelectProps) {
  return (
    <label className="form-field" htmlFor={id}>
      <span>{label}</span>
      <select id={id} {...props}>
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
    </label>
  )
}
