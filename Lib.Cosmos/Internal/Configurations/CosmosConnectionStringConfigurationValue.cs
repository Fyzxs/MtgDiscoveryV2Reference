﻿using Lib.UniversalCore.Configurations;

namespace Lib.Cosmos.Internal.Configurations;

public sealed class CosmosConnectionStringConfigurationValue : StringConfigurationValue
{
    public CosmosConnectionStringConfigurationValue() : base(new CosmosConnectionStringPrimaryConfigurationKey()) { }
}
