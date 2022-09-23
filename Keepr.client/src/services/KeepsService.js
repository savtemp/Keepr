import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get(`/api/keeps`);
    // logger.log("Getting keeps", res.data);
    AppState.keeps = res.data;
  }

  async getById(id) {
    const res = await api.get(`/api/keeps/${id}`);
    // logger.log("Getting keep by Id", res.data);
    AppState.activeKeep = res.data;
  }
}

export const keepsService = new KeepsService();
