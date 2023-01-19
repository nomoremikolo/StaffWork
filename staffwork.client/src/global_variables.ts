const isDev = process.env.NODE_ENV === 'development';
export const GraphQlEndpoint = isDev ? "https://localhost:7244/graphql" : "/graphql";
export const ImagesEndpoint = isDev ? "https://localhost:7244/Images" : "/Images";