import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get("/account");
      AppState.account = res.data;
    } catch (error) {
      logger.error("HAVE YOU STARTED YOUR SERVER YET???", err);
    }
  }

  async getAccountVaults() {
    try {
      const res = await api.get(`/account/vaults`);
      // logger.log("getting account vaults on page load", res.data);
      AppState.myVaults = res.data;
      logger.log("Getting account vaults on page load", AppState.account);
    } catch (error) {
      logger.log(error);
    }
  }
}

export const accountService = new AccountService();
