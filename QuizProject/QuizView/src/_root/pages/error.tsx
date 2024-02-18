import { Layout } from "@/layout"
import { useRouteError } from "react-router-dom"

export const ErrorPage = ()=>{
    const error = useRouteError() as any
    return (
        <div>
            <Layout></Layout>
            <h2>{error.status}</h2>
            <h2>{error.data}</h2>
        </div>
    )
}