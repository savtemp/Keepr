import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class ProfilesService {
  async getProfileById(id) {
    const res = await api.get(`/api/profiles/${id}`);
    // logger.log("Getting profile by id", res.data);
    AppState.activeProfile = res.data;
  }

  async getProfileKeeps(id) {
    const res = await api.get(`/api/profiles/${id}/keeps`);
    // logger.log("Getting profile keeps", res.data);
    AppState.profileKeeps = res.data;
  }

  async getProfileVaults(id) {
    const res = await api.get(`/api/profiles/${id}/vaults`);
    logger.log("Getting profile vaults", res.data);
    AppState.profileVaults = res.data;
  }
}

export const profilesService = new ProfilesService();
