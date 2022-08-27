import { writable } from "svelte/store";

export const socketUrl = "ws://localhost:9090";
export const storeSocketStatus = writable(0);