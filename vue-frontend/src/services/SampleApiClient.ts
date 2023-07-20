import { SampleApiUrl } from "@/configuration/SampleApiConfiguration";

export async function SignIn(token: string) {
  return await fetch(SampleApiUrl + "/user/sign-in?token=" + token, {
    method: "POST",
  }).then((r) => r.json());
}

export async function Register(username: string) {
  return await fetch(SampleApiUrl + "/user/create-token?user=" + username, {
    method: "POST",
  }).then((r) => r.json());
}
