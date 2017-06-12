namespace Entitas {

    public static class GroupExtension {

        /// Creates an EntityCollector for this group.
        public static Collector CreateCollector(
            this Group group,
            GroupEvent eventType = GroupEvent.Added) {
            return new Collector(group, eventType);
        }
    }
}
