const configuredApiUrl = import.meta.env.VITE_API_URL;
const apiUrl =
  configuredApiUrl && configuredApiUrl !== 'undefined'
    ? configuredApiUrl.replace(/\/$/, '')
    : 'http://localhost:5062';

type RequestOptions = RequestInit & {
  noContent?: boolean;
};

export async function api<TResponse>(
  path: string,
  options: RequestOptions = {},
): Promise<TResponse> {
  const response = await fetch(`${apiUrl}${path}`, {
    headers: {
      "Content-Type": "application/json",
      ...options.headers,
    },
    ...options,
  });

  if (!response.ok) {
    const body = await response.text();
    throw new Error(body || "Nao foi possivel concluir a operacao.");
  }

  if (options.noContent || response.status === 204) {
    return undefined as TResponse;
  }

  return response.json() as Promise<TResponse>;
}
