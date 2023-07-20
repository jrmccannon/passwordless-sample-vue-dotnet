import { useAuthStore } from "@/store";

export function isAuthenticated(to: any, from: any, next: any): boolean {
  const store = useAuthStore();

  return store.loggedIn ? next() : next("/login");
}
