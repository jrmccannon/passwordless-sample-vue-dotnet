import { SampleApiUrl } from "@/configuration/SampleApiConfiguration";
import Penguin from "@/models/Penguin";

export async function SignIn(token: string) {
  return await fetch(SampleApiUrl + "/user/sign-in?token=" + token, {
    method: "POST",
  })
    .then((response) => {
      console.log(response);
      if (!response.ok) {
        throw new Error(response.statusText);
      }

      return response.json();
    })
    .catch((error) => {
      console.error(error);
    });
}

export async function Register(
  username: string
): Promise<SuccessfulRegistrationResponse | ErrorRegistrationResponse> {
  return await fetch(SampleApiUrl + "/user/create-token?user=" + username, {
    method: "POST",
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error(response.statusText);
      }

      return response.json();
    })
    .then((r) => {
      return new SuccessfulRegistrationResponse(r.token);
    })
    .catch((error) => new ErrorRegistrationResponse(error));
}

export async function GetPenguins(token: string): Promise<Array<Penguin>> {
  return await fetch(SampleApiUrl + "/penguins", {
    headers: { Authorization: `Bearer ${token}` },
  })
    .then((r) => r.json())
    .catch((error) => console.error(error));
}

export class SuccessfulRegistrationResponse {
  token = "";

  constructor(token: string) {
    this.token = token;
  }
}

export class ErrorRegistrationResponse {
  errorMsg = "";

  constructor(msg: string) {
    this.errorMsg = msg;
  }
}
