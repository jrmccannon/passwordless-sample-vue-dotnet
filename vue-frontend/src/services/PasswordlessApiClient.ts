import { Client } from "@passwordlessdev/passwordless-client";
import {
  PasswordlessPublicKey,
  PasswordlessApiUrl,
} from "@/configuration/PasswordlessConfiguration";

export async function SignInUser(username: string) {
  const p = new Client({
    apiKey: PasswordlessPublicKey,
    apiUrl: PasswordlessApiUrl,
  });

  // Generate a verification token for the user.
  return await p.signinWithAlias(username);
}

export async function RegisterUser(token: string, alias: string) {
  const p = new Client({
    apiKey: PasswordlessPublicKey,
    apiUrl: PasswordlessApiUrl,
  });

  return await p.register(token, alias);
}
